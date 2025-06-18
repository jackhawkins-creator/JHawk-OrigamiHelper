const apiUrl = "/api/source";

export const getSources = () => {
  return fetch(apiUrl).then((res) => res.json());
};

export const getSourceById = (id) => {
  return fetch(`${apiUrl}/${id}`).then(res => res.json());
};
