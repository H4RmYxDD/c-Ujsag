import { useState } from "react";
import type { Author } from "../types/Author";
import { Button } from "react-bootstrap";
import apiClient from "../api/apiClient";
import { toast } from "react-toastify";

const RegisterPage = () => {
  const [author, setAuthor] = useState<Author>({
    email: "",
    password: "",
  });
  function register() {
    apiClient.post("/Account/register", author).then((res) => {
      switch (res.status) {
        case 201:
          toast.success("Sikeres regisztráció");
          break;
        case 400:
          toast.error("Bad request");
          break;
      }
    });
  }
  return (
    <>
      <div className="login-container">
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

        <Button onClick={register} className="btn-primary">
          Regisztráció
        </Button>

        <h5>
          Már van fiókod?{" "}
          <Button variant="link" href="/   ">
            Bejelentkezés
          </Button>
        </h5>
      </div>
    </>
  );
};

export default RegisterPage;
