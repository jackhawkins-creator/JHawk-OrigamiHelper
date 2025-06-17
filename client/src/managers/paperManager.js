const apiUrl = "/api/paper";

export const getPapers = () => {
  return fetch(apiUrl).then((res) => res.json());
};