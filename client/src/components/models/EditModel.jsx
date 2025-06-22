import { useEffect, useState } from "react";
import {
  Button, Form, FormGroup, Label, Input, FormText, Row, Col
} from "reactstrap";
import { useNavigate, useParams } from "react-router-dom";
import { getModelById, updateModel } from "../../managers/modelManager";
import { getComplexities } from "../../managers/complexityManager";
import { getSources } from "../../managers/sourceManager";
import { getPapers } from "../../managers/paperManager";

export default function EditModel({ loggedInUser }) {
  const { id } = useParams();
  const navigate = useNavigate();

  const [model, setModel] = useState(null);
  const [title, setTitle] = useState("");
  const [artist, setArtist] = useState("");
  const [sourceId, setSourceId] = useState("");
  const [stepCount, setStepCount] = useState(0);
  const [complexityId, setComplexityId] = useState("");
  const [selectedPapers, setSelectedPapers] = useState([]);
  const [modelImg, setModelImg] = useState("");
  const [imageFile, setImageFile] = useState(null);

  const [complexities, setComplexities] = useState([]);
  const [sources, setSources] = useState([]);
  const [papers, setPapers] = useState([]);

  useEffect(() => {
    getComplexities().then(setComplexities);
    getSources().then(setSources);
    getPapers().then(setPapers);
    getModelById(id).then(data => {
      if (data.userProfileId !== loggedInUser.id) {
        alert("Unauthorized: You can only edit your own models.");
        navigate(`/users/${loggedInUser.id}`);
        return;
      }
      setModel(data);
      setTitle(data.title);
      setArtist(data.artist);
      setSourceId(data.sourceId);
      setStepCount(data.stepCount);
      setComplexityId(data.complexityId);
      setSelectedPapers(data.modelPapers.map(mp => mp.paperId));
      setModelImg(data.modelImg);
    });
  }, [id, loggedInUser.id, navigate]);

  const handleFileChange = (e) => setImageFile(e.target.files[0]);

  const uploadImage = async () => {
    const formData = new FormData();
    formData.append("file", imageFile);

    const res = await fetch("/api/model/upload", {
      method: "POST",
      body: formData,
    });

    if (!res.ok) throw new Error("Failed to upload image");
    const data = await res.json();
    return `/Images/${data.fileName}`;
  };

  const handlePaperChange = (index, value) => {
    const updated = [...selectedPapers];
    updated[index] = value;
    setSelectedPapers(updated);
  };

  const handleAddPaper = () => setSelectedPapers([...selectedPapers, ""]);
  const handleRemovePaper = (index) => {
    const updated = [...selectedPapers];
    updated.splice(index, 1);
    setSelectedPapers(updated);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    let finalImgPath = modelImg;
    if (imageFile) {
      try {
        finalImgPath = await uploadImage();
      } catch (error) {
        alert("Image upload failed.");
        return;
      }
    }

    const updatedModel = {
      id: model.id,
      title,
      artist,
      sourceId: parseInt(sourceId),
      stepCount: parseInt(stepCount),
      complexityId: parseInt(complexityId),
      userProfileId: model.userProfileId,
      modelImg: finalImgPath,
      modelPapers: selectedPapers.map(pid => ({ paperId: parseInt(pid) })),
    };

    updateModel(updatedModel).then(() => {
      navigate(`/users/${loggedInUser.id}`);
    });
  };

  if (!model) return <p>Loading...</p>;

  return (
    <div className="container mt-4">
      <h2>Edit Model</h2>
      <Form onSubmit={handleSubmit}>
        <FormGroup>
          <Label for="title">Title</Label>
          <Input id="title" value={title} onChange={e => setTitle(e.target.value)} />
        </FormGroup>

        <FormGroup>
          <Label for="artist">Artist</Label>
          <Input id="artist" value={artist} onChange={e => setArtist(e.target.value)} />
        </FormGroup>

        <FormGroup>
          <Label for="source">Source</Label>
          <Input
            type="select"
            id="source"
            value={sourceId}
            onChange={e => setSourceId(e.target.value)}
          >
            <option value="">-- Select Source --</option>
            {sources.map(s => <option key={s.id} value={s.id}>{s.title}</option>)}
          </Input>
        </FormGroup>

        <FormGroup>
          <Label for="stepCount">Step Count</Label>
          <Input
            type="number"
            id="stepCount"
            value={stepCount}
            onChange={e => setStepCount(e.target.value)}
          />
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
                  onChange={(e) => handlePaperChange(index, e.target.value)}
                >
                  <option value="">-- Select Paper --</option>
                  {papers.map(p => (
                    <option key={p.id} value={p.id}>{p.brand}</option>
                  ))}
                </Input>
              </Col>
              <Col md={2}>
                <Button color="danger" onClick={() => handleRemovePaper(index)}>
                  Remove
                </Button>
              </Col>
            </Row>
          ))}
          <Button color="secondary" onClick={handleAddPaper}>Add Paper</Button>
        </FormGroup>

        <FormGroup>
          <Label for="modelImg">Image</Label>
          <Input type="file" id="modelImg" onChange={handleFileChange} />
          <FormText color="muted">Current Image: {modelImg}</FormText>
        </FormGroup>

        <Button color="primary" type="submit">Save Changes</Button>
      </Form>
    </div>
  );
}
