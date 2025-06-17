const apiUrl = "/api/source";

export const getSources = () => {
  return fetch(apiUrl).then((res) => res.json());
};