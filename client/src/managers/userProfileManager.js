const apiUrl = "/api/userprofile";

export const getUserProfileById = (id) => {
  return fetch(`${apiUrl}/${id}`).then(res => res.json());
};
