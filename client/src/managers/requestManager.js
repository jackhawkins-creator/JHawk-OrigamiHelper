const apiUrl = "/api/request";

export const getRequests = () => {
  return fetch(apiUrl).then((res) => res.json());
};

export const getRequestById = (id) => {
  return fetch(`${apiUrl}/${id}`).then(res => res.json());
};

export const createRequest = (request) => {
  return fetch(apiUrl, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(request),
  }).then((res) => res.json);
};

export const deleteRequest = (id) => {
  return fetch(`${apiUrl}/${id}`, {
    method: "DELETE"
  });
};

export const getRecentRequests = () => {
  return fetch("/api/request/recent").then((res) => res.json());
};