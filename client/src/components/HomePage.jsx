import { useEffect, useState } from "react";
import ModelCard from "./models/ModelCard";
import { getRecentModels } from "../managers/modelManager";

export default function HomePage() {
  const [models, setModels] = useState([]);

  useEffect(() => {
    getRecentModels().then(setModels);
  }, []);

  return (
    <div className="container mt-5">
      <h2 className="mb-4 text-center">Recently Added Models</h2>
      <div className="row g-4">
        {models.map((model) => (
          <div className="col-sm-6 col-md-4" key={model.id}>
            <ModelCard model={model} />
          </div>
        ))}
      </div>
    </div>
  );
}
