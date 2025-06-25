import { Card, CardBody, CardTitle, CardText } from "reactstrap";

function getYouTubeEmbedUrl(mediaUrl) {
  const match = mediaUrl.match(/(?:https?:\/\/)?(?:www\.)?youtube\.com\/watch\?v=([^&]+)/);
  return match ? `https://www.youtube.com/embed/${match[1]}` : null;
}

export default function ResponseCard({ response }) {
  const embedUrl = getYouTubeEmbedUrl(response.media);

  return (
    <Card color="dark" outline style={{ marginBottom: "12px" }}>
      <CardBody>
        <CardTitle tag="h6">
          Response to Step {response?.request?.stepNumber}
        </CardTitle>
        <CardText>{response.description}</CardText>
        {embedUrl ? (
          <div className="ratio ratio-16x9 mb-2">
            <iframe
              src={embedUrl}
              title="Video Response"
              allowFullScreen
            ></iframe>
          </div>
        ) : (
          <p>Invalid or missing video URL.</p>
        )}
        <CardText className="text-muted" style={{ fontSize: "0.8rem" }}>
          Posted on {new Date(response.createdAt).toLocaleDateString()}
        </CardText>
      </CardBody>
    </Card>
  );
}
