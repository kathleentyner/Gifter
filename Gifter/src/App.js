import React from "react";
import "./App.css";
import PostList from "./components/PostList";
import { PostSearch } from "./components/PostSearch";
import { PostForm } from "./components/PostForm";
import { BrowserRouter } from 'react-router-dom'
import { render } from "react-dom";

function App() {
 render(
  <BrowserRouter>
    <PostSearch/>
    <PostForm/>
    <PostList />
  </BrowserRouter>,
  document.getElementById('root')
)
}

export default App;