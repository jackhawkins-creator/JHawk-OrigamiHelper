import {
  Card,
  CardBody,
  CardTitle,
  CardText,
  CardSubtitle,
} from "reactstrap";

export default function RequestCard({ request }) {
  return (
    <Card color="dark" outline style={{ marginBottom: "10px" }}>
      <CardBody>
        <CardTitle tag="h5">{request.model?.title || "Unknown Model"}</CardTitle>
        <CardSubtitle className="mb-2 text-muted">
          Step {request.stepNumber}
        </CardSubtitle>
        <CardText>{request.description}</CardText>
        <CardText className="text-muted" style={{ fontSize: "0.8rem" }}>
          Submitted on {new Date(request.createdAt).toLocaleDateString()}
        </CardText>
      </CardBody>
    </Card>
  );
}
