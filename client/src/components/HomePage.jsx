import { useEffect, useState } from "react";
import ModelCard from "./models/ModelCard";
import { getRecentModels } from "../managers/modelManager";

export default function HomePage() {
  const [models, setModels] = useState([]);

  useEffect(() => {
    getRecentModels().then(setModels);
  }, []);

  return (
    <div className="container mt-4">
      <h2>Recently Added Models</h2>
      <div className="row">
        {models.map((model) => (
          <div className="col-md-4" key={model.id}>
            <ModelCard model={model} />
          </div>
        ))}
      </div>
    </div>
  );
}
