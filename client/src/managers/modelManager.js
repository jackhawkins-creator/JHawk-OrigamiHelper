const apiUrl = "/api/model";

export const getModels = () => {
  return fetch(apiUrl).then((res) => res.json());
};

export const getModelById = (id) => {
  return fetch(`${apiUrl}/${id}`).then(res => res.json());
};

export const createModel = (model) => {
  return fetch(apiUrl, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(model),
  }).then((res) => res.json);
};

export const updateModel = (model) => {
  return fetch(`${apiUrl}/${model.id}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(model),
  });
};

export const deleteModel = (id) => {
  return fetch(`${apiUrl}/${id}`, {
    method: "DELETE"
  });
};

export const getModelsByUserId = (userId) => {
  return fetch(`/api/model/user/${userId}`).then((res) => res.json());
};

export const getRecentModels = () => {
  return fetch("/api/model/recent").then((res) => res.json());
};
