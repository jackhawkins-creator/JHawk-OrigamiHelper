import { useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { createResponse } from "../../managers/responseManager";

export default function CreateResponse({ loggedInUser }) {
  const { requestId } = useParams();
  const [media, setMedia] = useState("");
  const [description, setDescription] = useState("");
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();

    const response = {
      requestId: parseInt(requestId),
      responderId: loggedInUser.id, // assuming this is UserProfile id
      media,
      description,
    };

    try {
      await createResponse(response);
      navigate(`/responses/${requestId}`);
    } catch (error) {
      console.error("Failed to create response:", error);
    }
  };

  return (
    <div className="container mt-5">
      <h3>Add a Video Response for Step Help</h3>
      <form onSubmit={handleSubmit}>
        <div className="mb-3">
          <label htmlFor="media" className="form-label">
            YouTube Video URL
          </label>
          <input
            type="url"
            className="form-control"
            id="media"
            value={media}
            onChange={(e) => setMedia(e.target.value)}
            placeholder="https://www.youtube.com/watch?v=..."
            required
          />
        </div>
        <div className="mb-3">
          <label htmlFor="description" className="form-label">
            Short Description
          </label>
          <textarea
            className="form-control"
            id="description"
            rows="3"
            value={description}
            onChange={(e) => setDescription(e.target.value)}
            required
          ></textarea>
        </div>
        <button type="submit" className="btn btn-success">
          Submit Response
        </button>
      </form>
    </div>
  );
}
