import { useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import {
  Button,
  Form,
  FormGroup,
  Label,
  Input,
  Container,
  Alert,
} from "reactstrap";
import { createRequest } from "../../managers/requestManager";
import { getModelById } from "../../managers/modelManager";
import { useEffect } from "react";

export default function CreateRequest({ loggedInUser }) {
  const { id: modelId } = useParams(); // id of the model from route
  const navigate = useNavigate();

  const [stepNumber, setStepNumber] = useState("");
  const [description, setDescription] = useState("");
  const [model, setModel] = useState(null);
  const [error, setError] = useState(null);

  useEffect(() => {
    getModelById(modelId)
      .then(setModel)
      .catch((err) => {
        console.error("Error loading model info:", err);
        setError("Could not load model information.");
      });
  }, [modelId]);

  const handleSubmit = (e) => {
    e.preventDefault();

    const parsedStepNumber = parseInt(stepNumber);

    if (!parsedStepNumber || parsedStepNumber < 1 || parsedStepNumber > model?.stepCount) {
      setError(`Step number must be between 1 and ${model?.stepCount}`);
      return;
    }

    const newRequest = {
      userId: loggedInUser.id,
      modelId: parseInt(modelId),
      stepNumber: parsedStepNumber,
      description,
    };

    createRequest(newRequest)
      .then(() => navigate("/requests"))
      .catch((err) => {
        console.error("Error creating request:", err);
        setError("There was an error submitting your request.");
      });
  };

  if (!model) {
    return <p>Loading model information...</p>;
  }

  return (
    <Container className="mt-5">
      <h2>Create Help Request for: <strong>{model.title}</strong></h2>

      {error && <Alert color="danger">{error}</Alert>}

      <Form onSubmit={handleSubmit}>
        <FormGroup>
          <Label for="stepNumber">Step Number</Label>
          <Input
            type="number"
            name="stepNumber"
            id="stepNumber"
            min="1"
            max={model.stepCount}
            value={stepNumber}
            onChange={(e) => setStepNumber(e.target.value)}
            required
          />
        </FormGroup>

        <FormGroup>
          <Label for="description">Description of the issue</Label>
          <Input
            type="textarea"
            name="description"
            id="description"
            rows="5"
            value={description}
            onChange={(e) => setDescription(e.target.value)}
            required
          />
        </FormGroup>

        <Button color="primary" type="submit">Submit Request</Button>
      </Form>
    </Container>
  );
}
