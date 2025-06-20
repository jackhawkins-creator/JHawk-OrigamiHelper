import { Card, CardBody, CardTitle, CardText, CardSubtitle } from "reactstrap";
import { Link } from "react-router-dom";

export default function ModelCard({ model }) {
  return (
    <Link to={`/models/${model.id}`} style={{ textDecoration: "none", color: "inherit" }}>
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
        </CardBody>
      </Card>
    </Link>
  );
}
