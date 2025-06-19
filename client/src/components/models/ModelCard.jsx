import {
  Card,
  CardBody,
  CardTitle,
  CardText,
  CardSubtitle,
} from "reactstrap";

export default function ModelCard({ model }) {
  return (
    <Card className="mb-4">
      <img src={model.modelImg} alt={model.title} className="card-img-top" />
      <CardBody>
        <CardTitle tag="h5">{model.title}</CardTitle>
        <CardSubtitle className="mb-2 text-muted">
          {model.artist}
        </CardSubtitle>
        <CardText>
          Steps: {model.stepCount}
        </CardText>
      </CardBody>
    </Card>
  );
}
