const apiUrl = "/api/userprofile";

export const getUserProfileById = (id) => {
  return fetch(`${apiUrl}/${id}`).then(res => res.json());
};

export const updateCurrentUserProfile = (profile) => {
  return fetch(`${apiUrl}/me`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(profile),
  });
};
