import { useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { createResponse } from "../../managers/responseManager";

export default function CreateResponse({ loggedInUser }) {
  const { requestId } = useParams();
  const [description, setDescription] = useState("");
  const [videoFile, setVideoFile] = useState(null);
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();

    const formData = new FormData();
    formData.append("videoFile", videoFile);
    formData.append("requestId", requestId);
    formData.append("responderId", loggedInUser.id);
    formData.append("description", description);

    try {
      const res = await fetch("/api/response/upload", {
        method: "POST",
        body: formData,
      });

      if (!res.ok) throw new Error("Failed to upload video response.");

      navigate(`/responses/${requestId}`);
    } catch (err) {
      console.error(err);
    }
  };

  return (
    <div className="container mt-5">
      <h3>Add a Video Response</h3>
      <form onSubmit={handleSubmit} encType="multipart/form-data">
        <div className="mb-3">
          <label htmlFor="videoFile" className="form-label">
            Upload Video File
          </label>
          <input
            type="file"
            accept="video/*"
            className="form-control"
            onChange={(e) => setVideoFile(e.target.files[0])}
            required
          />
        </div>
        <div className="mb-3">
          <label htmlFor="description" className="form-label">
            Description
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