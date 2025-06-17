const apiUrl = "/api/model";

export const getModels = () => {
  return fetch(apiUrl).then((res) => res.json());
};