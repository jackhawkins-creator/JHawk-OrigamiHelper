import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { getUserProfileById } from "../../managers/userProfileManager";
import { getModelsByUserId } from "../../managers/modelManager";
import { Spinner } from "reactstrap";
import ModelCard from "../models/ModelCard";

export default function UserProfile({loggedInUser}) {
  const { id } = useParams();
  const [profile, setProfile] = useState(null);
  const [models, setModels] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    Promise.all([
      getUserProfileById(id),
      getModelsByUserId(id)
    ])
      .then(([userData, modelData]) => {
        setProfile(userData);
        setModels(modelData);
      })
      .catch((error) => {
        console.error("Failed to load user profile or models", error);
      })
      .finally(() => setLoading(false));
  }, [id]);

  if (loading) return <Spinner color="primary" />;
  if (!profile) return <p>User has no models.</p>;

  return (
    <div className="container mt-4">
      <div className="mb-4">
        <h2>{profile.firstName} {profile.lastName}</h2>
        <p><strong>Email:</strong> {profile.email}</p>
        <p><strong>Address:</strong> {profile.address}</p>
      </div>

      <h4>Models by {profile.firstName}</h4>
      <div className="row">
        {models.map(model => (
          <div key={model.id} className="col-md-4">
            <ModelCard model={model} loggedInUser={loggedInUser} />
          </div>
        ))}
      </div>
    </div>
  );
}
