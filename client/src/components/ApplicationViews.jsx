import { Route, Routes } from "react-router-dom";
import { AuthorizedRoute } from "./auth/AuthorizedRoute";
import Login from "./auth/Login";
import Register from "./auth/Register";
import ModelList from "./models/ModelList";
import HomePage from "./HomePage";
import CreateModel from "./models/CreateModel";
import ModelDetails from "./models/ModelDetails";
import UserProfile from "./profiles/UserProfileDetails";
import EditModel from "./models/EditModel";
import RequestList from "./requests/RequestList";
import CreateRequest from "./requests/CreateRequest";

export default function ApplicationViews({ loggedInUser, setLoggedInUser }) {
  return (
    <Routes>
      <Route path="/">
        <Route
          index
          element={
            <AuthorizedRoute loggedInUser={loggedInUser}>
              <HomePage />
            </AuthorizedRoute>
          }
        />
        <Route
          path="models"
          element={
            <AuthorizedRoute loggedInUser={loggedInUser}>
              <ModelList />
            </AuthorizedRoute>
          }
        />
        <Route
          path="models/create"
          element={
            <AuthorizedRoute loggedInUser={loggedInUser}>
              <CreateModel loggedInUser={loggedInUser} />
            </AuthorizedRoute>
          }
        />

        <Route
          path="models/:id"
          element={
            <AuthorizedRoute loggedInUser={loggedInUser}>
              <ModelDetails loggedInUser={loggedInUser} />
            </AuthorizedRoute>
          }
        />

        <Route
          path="users/:id"
          element={
            <AuthorizedRoute loggedInUser={loggedInUser}>
              <UserProfile loggedInUser={loggedInUser} />
            </AuthorizedRoute>
          }
        />

        <Route
          path="models/:id/edit"
          element={
            <AuthorizedRoute loggedInUser={loggedInUser}>
              <EditModel loggedInUser={loggedInUser} />
            </AuthorizedRoute>
          }
        />

        <Route
          path="requests"
          element={
            <AuthorizedRoute loggedInUser={loggedInUser}>
              <RequestList />
            </AuthorizedRoute>
          }
        />

        <Route
          path="models/:id/help-request"
          element={
            <AuthorizedRoute loggedInUser={loggedInUser}>
              <CreateRequest loggedInUser={loggedInUser} />
            </AuthorizedRoute>
          }
        />

        <Route
          path="login"
          element={<Login setLoggedInUser={setLoggedInUser} />}
        />
        <Route
          path="register"
          element={<Register setLoggedInUser={setLoggedInUser} />}
        />
      </Route>
      <Route path="*" element={<p>Whoops, nothing here...</p>} />
    </Routes>
  );
}
