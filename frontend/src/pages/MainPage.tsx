import { useEffect, useState } from "react";
import type { Article } from "../types/Article";
import apiClient from "../api/apiClient";
import { toast } from "react-toastify";
import { Col, Card, Button, Container, Row } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import "../design/MainPage.css";

const MainPage = () => {
  const [articles, setArticles] = useState<Array<Article>>([]);
  const nav = useNavigate();
  const logout = () => {
    localStorage.removeItem("accessToken");
    window.location.href = "/";
  };
  function fetchArticles() {
    apiClient
      .get("/list")
      .then((res) => setArticles(res.data))
      .catch((reas) => toast.error(reas));
  }
  useEffect(() => {
    fetchArticles();
  }, []);
  const generateCard = (a: Article) => {
    return (
      <Col>
        <Card style={{ width: "18rem" }}>
          <Card.Body>
            <Card.Title>{a.title}</Card.Title>
            <Card.Text>{a.content}</Card.Text>
            <Button onClick={() => nav(`/article/${a.id}`)} variant="success">
              Megtekintés
            </Button>
          </Card.Body>
        </Card>
      </Col>
    );
  };
  return (
    <>
      <Container>
        <Row xs={"auto"} md={"auto"} className="g-4">
          {articles.map((i) => generateCard(i))}
        </Row>
      </Container>

      <Button variant="danger" onClick={logout}>
        Kijelentkezés
      </Button>
    </>
  );
};

export default MainPage;
