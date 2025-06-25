import { useState, useEffect } from "react";
import ResponseCard from "./ResponseCard";
import { getResponsesByRequestId } from "../../managers/responseManager";
import { Link } from "react-router-dom";

export default function ResponseList({ requestId }) {
  const [responses, setResponses] = useState([]);

  useEffect(() => {
    if (requestId) {
      getResponsesByRequestId(requestId).then(setResponses);
    }
  }, [requestId]);

  return (
    <div className="container mt-4">
      <h3 className="mb-3">Video Responses</h3>
      <Link
        to={`/responses/${requestId}/create`}
        className="btn btn-outline-primary mb-3"
      >
        Add Video Response
      </Link>
      {responses.length === 0 ? (
        <p>No responses yet. Be the first to help!</p>
      ) : (
        responses.map((response) => (
          <ResponseCard key={`response-${response.id}`} response={response} />
        ))
      )}
    </div>
  );
}
