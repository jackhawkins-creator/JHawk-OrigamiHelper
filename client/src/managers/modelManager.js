const apiUrl = "/api/model";

export const getModels = () => {
  return fetch(apiUrl).then((res) => res.json());
};

export const getModelById = (id) => {
  return fetch(`${apiUrl}/${id}`).then(res => res.json());
};
