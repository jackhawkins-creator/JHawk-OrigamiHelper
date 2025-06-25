import { useEffect, useState } from "react";
import ModelCard from "./models/ModelCard";
import RequestCard from "./requests/RequestCard";
import { getRecentModels } from "../managers/modelManager";
import { getRecentRequests } from "../managers/requestManager";

export default function HomePage() {
  const [models, setModels] = useState([]);
  const [requests, setRequests] = useState([]);

  useEffect(() => {
    getRecentModels().then(setModels);
    getRecentRequests().then(setRequests);
  }, []);

  return (
    <div className="container mt-5">
      <h2 className="mb-4 text-center">Recently Added Models</h2>
      <div className="row g-4 mb-5">
        {models.map((model) => (
          <div className="col-sm-6 col-md-4" key={model.id}>
            <ModelCard model={model} />
          </div>
        ))}
      </div>

      <h2 className="mb-4 text-center">Recent Help Requests</h2>
      <div className="row g-4">
        {requests.map((request) => (
          <div className="col-sm-12 col-md-6 col-lg-4" key={request.id}>
            <RequestCard request={request} />
          </div>
        ))}
      </div>
    </div>
  );
}
