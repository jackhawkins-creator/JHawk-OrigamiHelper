const apiUrl = "/api/complexity";

export const getComplexities = () => {
  return fetch(apiUrl).then((res) => res.json());
};