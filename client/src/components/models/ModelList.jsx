import { Link } from "react-router-dom";
import { Button } from "reactstrap";
import ModelCard from "./ModelCard";
import { getModels } from "../../managers/modelManager";
import { useState, useEffect } from "react";


export default function ModelList() {
  const [models, setModels] = useState([]);


  useEffect(() => {
    getModels().then(setModels);
  }, []);


  return (
    <div className="container mt-4">
      <div className="d-flex justify-content-between align-items-center mb-3">
        <h2>Origami Models</h2>
        <Link to="/models/create">
          <Button color="success">+ Create New Model</Button>
        </Link>
      </div>
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
