import { useEffect, useState } from "react";
import ModelCard from "./ModelCard";
import { getModels } from "../../managers/modelManager";

export default function ModelList() {
  const [models, setModels] = useState([]);

  useEffect(() => {
    getModels().then(setModels);
  }, []);

  return (
    <div className="container mt-4">
      <h2>Origami Models</h2>
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
