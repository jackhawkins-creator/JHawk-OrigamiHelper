import { Card, CardBody, CardTitle, CardText, CardSubtitle } from "reactstrap";
import { Link } from "react-router-dom";

export default function ModelCard({ model, loggedInUser }) {
  const isOwner = loggedInUser?.id === model.userProfileId;

  return (
    <Card className="mb-4 hover-shadow">
      <img
        src={`https://localhost:5001${model.modelImg}`}
        alt={model.title}
        className="card-img-top"
      />
      <CardBody>
        <CardTitle tag="h5">{model.title}</CardTitle>
        <CardSubtitle className="mb-2 text-muted">{model.artist}</CardSubtitle>
        <CardText>Steps: {model.stepCount}</CardText>
        {isOwner && (
          <Link to={`/models/${model.id}/edit`} className="btn btn-sm btn-outline-primary">
            Edit
          </Link>
        )}
      </CardBody>
    </Card>
  );
}
