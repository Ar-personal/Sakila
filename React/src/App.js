import { BrowserRouter as Router, Routes, Route, renderMatches} from 'react-router-dom';
import { default as Navigation } from "./components/Navigation";
import { default as Footer } from "./components/Footer";
import { default as Home } from "./components/Home";
import { default as About } from "./components/About";
import { default as Contact } from "./components/Contact";
import { default as Blog } from "./components/blog/Blog";
import { default as Posts } from "./components/blog/Posts";
import { default as Post } from "./components/blog/Post";

function App() {
  return (
   <Router>
    <Navigation />
    <Routes>
      <Route exact path="/" element={<Home />} />
      <Route path="/about" element={<About />} />
      <Route path="/contact" element={<Contact />} />
      <Route path="/blog" element={<Blog />}>
        <Route path="" element={<Posts />} />
        <Route path=":postSlug" element={<Post />} />
      </Route>
    </Routes>
    <Footer />
  </Router>
  );
};

export default App;
