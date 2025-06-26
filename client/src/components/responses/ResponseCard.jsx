import { Card, CardBody, CardTitle, CardText } from "reactstrap";

function isYouTubeUrl(url) {
  return url.includes("youtube.com");
}

export default function ResponseCard({ response }) {
  const embedUrl = isYouTubeUrl(response.media)
  ? `https://www.youtube.com/embed/${new URLSearchParams(new URL(response.media).search).get("v")}`
  : `https://localhost:5001${response.media}`; // << prepend your full backend URL here


  return (
    <Card color="dark" outline style={{ marginBottom: "12px" }}>
      <CardBody>
        <CardTitle tag="h6">
          Response to Step {response?.request?.stepNumber}
        </CardTitle>
        <CardText>{response.description}</CardText>

        {isYouTubeUrl(response.media) ? (
          <div className="ratio ratio-16x9 mb-2">
            <iframe src={embedUrl} title="Video Response" allowFullScreen></iframe>
          </div>
        ) : (
          <div className="ratio ratio-16x9 mb-2">
            <video controls>
              <source src={embedUrl} type="video/mp4" />
              Your browser does not support the video tag.
            </video>
          </div>
        )}

        <CardText className="text-muted" style={{ fontSize: "0.8rem" }}>
          Posted on {new Date(response.createdAt).toLocaleDateString()}
        </CardText>
      </CardBody>
    </Card>
  );
}
