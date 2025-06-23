import {
  Card,
  CardBody,
  CardTitle,
  CardText,
  CardSubtitle,
  Button,
} from "reactstrap";
import { Link, useNavigate } from "react-router-dom";
import { deleteModel } from "../../managers/modelManager";

export default function ModelCard({ model, loggedInUser }) {
  const isOwner = loggedInUser?.id === model.userProfileId;
  const navigate = useNavigate();

  const handleDelete = async (e) => {
    e.preventDefault(); // prevent link navigation
    if (window.confirm("Are you sure you want to delete this model?")) {
      await deleteModel(model.id);
      // Optionally reload page or trigger re-fetch from parent
      navigate(0); // reload current page
    }
  };

  return (
    <Card className="model-card h-100">
      <Link
        to={`/models/${model.id}`}
        style={{ textDecoration: "none", color: "inherit" }}
      >
        <img
          src={`https://localhost:5001${model.modelImg}`}
          alt={model.title}
          className="card-img-top model-card-img"
        />
      </Link>
      <CardBody className="model-card-body d-flex flex-column">
        <CardTitle tag="h5">{model.title}</CardTitle>
        <CardSubtitle className="mb-2 text-muted">{model.artist}</CardSubtitle>
        <CardText>Steps: {model.stepCount}</CardText>

        {isOwner && (
          <div className="model-card-footer mt-3">
            <Link
              to={`/models/${model.id}/edit`}
              className="btn btn-sm btn-outline-primary me-2"
              onClick={(e) => e.stopPropagation()}
            >
              Edit
            </Link>
            <Button color="danger" size="sm" onClick={handleDelete}>
              Delete
            </Button>
          </div>
        )}
      </CardBody>
    </Card>
  );
}
