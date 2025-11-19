import { useState } from "react";
import type { Author } from "../types/Author";
import { Button } from "react-bootstrap";
import apiClient from "../api/apiClient";
import { toast } from "react-toastify";

const LoginPage = () => {
  const [author, setAuthor] = useState<Author>({
    email: "",
    password: "",
  });
  function login() {
    apiClient.post("/authors").then((res) => {
      switch (res.status) {
        case 200:
          toast.success("Sikeres belépés");
          
        case 400:
          toast.error("Bad request");
      }
    });
  }
  return (
    <>
      <div>
        <p>E-mail cím</p>
        <input
          type="text"
          onChange={(e) => setAuthor({ ...author, email: e.target.value })}
        />
        <p>Jelszó</p>
        <input
          type="password"
          onChange={(e) => setAuthor({ ...author, password: e.target.value })}
        />
        <Button onClick={login}>Bejelentkezés</Button>
      </div>
    </>
  );
};

export default LoginPage;
