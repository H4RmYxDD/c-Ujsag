import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import LoginPage from "./pages/LoginPage.tsx";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import ErrorPage from "./pages/ErrorPage.tsx";
import RegisterPage from "./pages/RegisterPage.tsx";
import { ToastContainer } from "react-toastify";
import MainPage from "./pages/MainPage.tsx";

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<LoginPage />} />
        <Route path="/register" element={<RegisterPage />} />
        <Route path="/main" element={<MainPage />} />
        <Route path="*" element={<ErrorPage />} />
      </Routes>
    </BrowserRouter>
    <ToastContainer theme="colored" />
  </StrictMode>
);
