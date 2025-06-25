const apiUrl = "/api/response";

export const getResponsesByRequestId = (requestId) => {
  return fetch(`${apiUrl}/request/${requestId}`)
    .then((res) => {
      if (!res.ok) {
        throw new Error(`Failed to fetch responses for request ${requestId}`);
      }
      return res.json();
    });
};


export const createResponse = (response) => {
  return fetch(apiUrl, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(response),
  }).then((res) => res.json);
};

export const deleteResponse = (id) => {
  return fetch(`${apiUrl}/${id}`, {
    method: "DELETE"
  });
};
