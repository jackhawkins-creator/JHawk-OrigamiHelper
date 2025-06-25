import { useState, useEffect } from "react";
import RequestCard from "./RequestCard";
import { getRequests } from "../../managers/requestManager";

export default function RequestList() {
  const [requests, setRequests] = useState([]);

  useEffect(() => {
    getRequests().then(setRequests);
  }, []);

  return (
    <div className="container mt-5">
      <h2 className="mb-4 text-center">Help Requests</h2>
      {requests.length === 0 ? (
        <p>No help requests found.</p>
      ) : (
        requests.map((request) => (
          <RequestCard key={request.Id} request={request} />
        ))
      )}
    </div>
  );
}
