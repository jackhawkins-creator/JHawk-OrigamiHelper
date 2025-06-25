import { useState, useEffect } from "react";
import ResponseCard from "./ResponseCard";
import { getResponsesByRequestId } from "../../managers/responseManager";

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
