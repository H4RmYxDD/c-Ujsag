import axios from "axios";

export const baseURL = "http://localhost:5038";

const apiClient = axios.create({
  baseURL,
  headers: {
    "Content-Type": "application/json",
  },
});

export default apiClient;
