const apiUrl = "/api/paper";

export const getPapers = () => {
  return fetch(apiUrl).then((res) => res.json());
};

export const getPaperById = (id) => {
  return fetch(`${apiUrl}/${id}`).then(res => res.json());
};
