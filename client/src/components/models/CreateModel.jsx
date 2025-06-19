// client/src/components/models/CreateModel.jsx
import { useEffect, useState } from "react";
import {
  Button, Form, FormGroup, Label, Input, FormText, Row, Col
} from "reactstrap";
import { getComplexities } from "../../managers/complexityManager";
import { getSources } from "../../managers/sourceManager";
import { getPapers } from "../../managers/paperManager";
import { createModel } from "../../managers/modelManager";
import { useNavigate } from "react-router-dom";

export default function CreateModel({ loggedInUser }) {
  const navigate = useNavigate();
  const [title, setTitle] = useState("");
  const [artist, setArtist] = useState("");
  const [sourceId, setSourceId] = useState("");
  const [stepCount, setStepCount] = useState(0);
  const [complexityId, setComplexityId] = useState("");
  const [modelImg, setModelImg] = useState("");
  const [selectedPapers, setSelectedPapers] = useState([]);

  const [complexities, setComplexities] = useState([]);
  const [sources, setSources] = useState([]);
  const [papers, setPapers] = useState([]);

  useEffect(() => {
    getComplexities().then(setComplexities);
    getSources().then(setSources);
    getPapers().then(setPapers);
  }, []);

  const handleAddPaper = () => {
    setSelectedPapers([...selectedPapers, ""]);
  };

  const handleRemovePaper = (index) => {
    const updated = [...selectedPapers];
    updated.splice(index, 1);
    setSelectedPapers(updated);
  };

  const handlePaperChange = (index, value) => {
    const updated = [...selectedPapers];
    updated[index] = value;
    setSelectedPapers(updated);
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    const model = {
      title,
      artist,
      sourceId: parseInt(sourceId),
      complexityId: parseInt(complexityId),
      stepCount: parseInt(stepCount),
      userProfileId: loggedInUser.id,
      modelImg, // just file name or placeholder string
      modelPapers: selectedPapers.map(pid => ({ paperId: parseInt(pid) })),
    };

    createModel(model).then(() => navigate("/models"));
  };

  return (
    <div className="container mt-4">
      <h2>Create New Model</h2>
      <Form onSubmit={handleSubmit}>
        <FormGroup>
          <Label for="title">Model Title</Label>
          <Input id="title" value={title} onChange={e => setTitle(e.target.value)} required />
        </FormGroup>

        <FormGroup>
          <Label for="artist">Artist</Label>
          <Input id="artist" value={artist} onChange={e => setArtist(e.target.value)} required />
        </FormGroup>

        <FormGroup>
          <Label for="sourceId">Source</Label>
          <Input type="select" id="sourceId" value={sourceId} onChange={e => setSourceId(e.target.value)} required>
            <option value="">-- Select Source --</option>
            {sources.map(src => <option key={src.id} value={src.id}>{src.title}</option>)}
          </Input>
        </FormGroup>

        <FormGroup>
          <Label for="stepCount">Step Count</Label>
          <Input type="number" id="stepCount" value={stepCount} onChange={e => setStepCount(e.target.value)} required />
        </FormGroup>

        <FormGroup tag="fieldset">
          <legend>Complexity</legend>
          {complexities.map(c => (
            <FormGroup check key={c.id}>
              <Input
                type="radio"
                name="complexity"
                value={c.id}
                checked={parseInt(complexityId) === c.id}
                onChange={e => setComplexityId(e.target.value)}
              />
              <Label check>{c.difficulty}</Label>
            </FormGroup>
          ))}
        </FormGroup>

        <FormGroup>
          <Label>Paper Types</Label>
          {selectedPapers.map((paperId, index) => (
            <Row key={index} className="mb-2">
              <Col md={10}>
                <Input
                  type="select"
                  value={paperId}
                  onChange={e => handlePaperChange(index, e.target.value)}
                >
                  <option value="">-- Select Paper --</option>
                  {papers.map(p => <option key={p.id} value={p.id}>{p.brand}</option>)}
                </Input>
              </Col>
              <Col md={2}>
                <Button color="danger" onClick={() => handleRemovePaper(index)}>Remove</Button>
              </Col>
            </Row>
          ))}
          <Button color="secondary" onClick={handleAddPaper}>Add Paper</Button>
        </FormGroup>

        <FormGroup>
          <Label for="modelImg">Model Image (filename only)</Label>
          <Input id="modelImg" value={modelImg} onChange={e => setModelImg(e.target.value)} />
          <FormText color="muted">Just use the file name for now (e.g. "dragon.png")</FormText>
        </FormGroup>

        <Button color="primary" type="submit">Create Model</Button>
      </Form>
    </div>
  );
}
