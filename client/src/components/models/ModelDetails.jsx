import { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import {
  Card,
  CardBody,
  CardTitle,
  CardText,
  CardSubtitle,
  Spinner,
} from "reactstrap";
import { getModelById } from "../../managers/modelManager";

export default function ModelDetails() {
  const { id } = useParams();
  const [model, setModel] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    getModelById(id)
      .then((data) => {
        setModel(data);
        setLoading(false);
      })
      .catch((err) => {
        console.error("Failed to fetch model details:", err);
        setLoading(false);
      });
  }, [id]);

  if (loading) {
    return <Spinner color="primary" />;
  }

  if (!model) {
    return <p>Model not found.</p>;
  }

  const {
    modelImg,
    title,
    artist,
    stepCount,
    complexity,
    source,
    modelPapers,
    userProfile,
  } = model;

  const paperBrands = modelPapers
    ?.map((mp) => mp.paper?.brand)
    .filter(Boolean)
    .join(", ");

  return (
    <Card className="mt-4">
      <img
        src={`https://localhost:5001${modelImg}`}
        alt={title}
        className="card-img-top"
      />
      <CardBody>
        <CardTitle tag="h3">{title}</CardTitle>
        <CardSubtitle className="mb-2 text-muted">
          by {artist || "Unknown Artist"}
        </CardSubtitle>
        <CardText>
          <strong>Uploaded by:</strong>{" "}
          <Link to={`/users/${userProfile?.id}`}>
            {userProfile?.firstName} {userProfile?.lastName}
          </Link>
        </CardText>

        <CardText>
          <strong>Complexity:</strong> {complexity?.difficulty}
        </CardText>
        <CardText>
          <strong>Recommended Paper(s):</strong> {paperBrands || "N/A"}
        </CardText>
        <CardText>
          <strong>Source:</strong> {source?.title}
        </CardText>
        <CardText>
          <strong>Total Steps:</strong> {stepCount}
        </CardText>
      </CardBody>
    </Card>
  );
}
