import { useState, useEffect } from "react";
import type { Article } from "../types/Article";
import apiClient from "../api/apiClient";
import { useNavigate, useParams } from "react-router-dom";
import { toast } from "react-toastify";
import { Container, Card, Button, Spinner } from "react-bootstrap";
import "../design/ArticlePage.css";

const ArticlePage = () => {
  const [article, setArticle] = useState<Article>();
  const { id } = useParams();
  const nav = useNavigate();

  function fetchArticle() {
    apiClient
      .get(`/get/${id}`)
      .then((res) => setArticle(res.data))
      .catch((reas) => toast.error(reas));
  }

  useEffect(() => {
    fetchArticle();
  }, []);

  if (!article)
    return (
      <Container className="article-loading">
        <Spinner animation="border" variant="primary" />
      </Container>
    );

  return (
    <Container className="article-container">
      <Card className="article-card">
        <Card.Body>
          <Card.Title>{article.title}</Card.Title>
          <Card.Text>{article.content}</Card.Text>

          <Button variant="secondary" onClick={() => nav(-1)}>
            Vissza
          </Button>
        </Card.Body>
      </Card>
    </Container>
  );
};

export default ArticlePage;
