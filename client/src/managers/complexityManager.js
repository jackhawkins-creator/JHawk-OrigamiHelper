const apiUrl = "/api/complexity";

export const getComplexities = () => {
  return fetch(apiUrl).then((res) => res.json());
};

export const getComplexityById = (id) => {
  return fetch(`${apiUrl}/${id}`).then(res => res.json());
};
