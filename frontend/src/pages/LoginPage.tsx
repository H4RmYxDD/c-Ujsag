import { useState } from "react";
import type { Author } from "../types/Author";
import { Button } from "react-bootstrap";
import apiClient from "../api/apiClient";
import { toast } from "react-toastify";
import "./MainPage.css";

const LoginPage = () => {
  const [author, setAuthor] = useState<Author>({
    email: "",
    password: "",
  });
  function login() {
    apiClient
      .post("/Accounts/login")
      .then((res) => {
        switch (res.status) {
          case 200:
            toast.success("Sikeres belépés");
            break;
          case 400:
            toast.error("Bad request");
            break;
          case 401:
            toast.error("Még nincs regisztrálva az oldalon");
            break;
          case 404:
            toast.error("Sajnáljuk, de a kért oldal nem érhető el jelenleg");
            break;
          default:
            toast.error("Valami hiba történt");
            break;
        }
      })
      .catch((reason) => toast.error(reason));
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

        <Button onClick={login} className="btn-primary">
          Bejelentkezés
        </Button>
        <h5>
          Még nincs fiókod?{" "}
          <Button variant="link" href="/register">
            Regisztráció
          </Button>
        </h5>
      </div>
    </>
  );
};

export default LoginPage;
