
/*import React, { useState } from "react";
import "./App.css";
import calcImage from "./assets/logo.png";

function App() {
  const [num1, setNum1] = useState("");
  const [num2, setNum2] = useState("");
  const [operation, setOperation] = useState("add");
  const [result, setResult] = useState(null);

  const handleCalculate = async () => {
    if (num1 === "" || num2 === "") {
      alert("Please enter both numbers");
      return;
    }

    try {
      const url = `http://localhost:5095/Calculator/${operation}?a=${num1}&b=${num2}`;
      const response = await fetch(url);

      if (!response.ok) {
        const errMsg = await response.text();
        setResult("Error: " + errMsg);
        return;
      }

      const data = await response.text(); // backend sends plain text
      setResult(data);
    } catch (error) {
      setResult("Error: Could not connect to backend");
      console.error(error);
    }
  };

  const handleKeyPress = (e) => {
    if (e.key === "Enter") handleCalculate();
  };

  return (
    <>
      <div className="header">CalcMate</div>
      <div className="container">
        <div className="calculator-main">
          <div className="calculator">
            <h1>CalcMate</h1>
            <input
              type="number"
              placeholder="First Number"
              value={num1}
              onChange={(e) => setNum1(e.target.value)}
              onKeyDown={handleKeyPress}
            />
            <input
              type="number"
              placeholder="Second Number"
              value={num2}
              onChange={(e) => setNum2(e.target.value)}
              onKeyDown={handleKeyPress}
            />
            <select value={operation} onChange={(e) => setOperation(e.target.value)}>
              <option value="add">Add</option>
              <option value="subtract">Subtract</option>
              <option value="multiply">Multiply</option>
              <option value="divide">Divide</option>
            </select>
            <button onClick={handleCalculate}>Calculate</button>
            
           <a href="/HistoryPage">Go To History</a>

            {result !== null && <h2>Result: {result}</h2>}
          </div>
          <div className="image">
            <img src={calcImage} alt="Calculator" />
          </div>
        </div>
      </div>
      <div className="footer"></div>
    </>
  );
}

export default App;*/

/*import React, { useState } from "react";
import { BrowserRouter as Router, Routes, Route, Link } from "react-router-dom";
import "./App.css";
import calcImage from "./assets/logo.png";
import HistoryPage from "./HistoryPage";

function Calculator() {
  const [num1, setNum1] = useState("");
  const [num2, setNum2] = useState("");
  const [operation, setOperation] = useState("add");
  const [result, setResult] = useState(null);

  const handleCalculate = async () => {
    if (num1 === "" || num2 === "") {
      alert("Please enter both numbers");
      return;
    }

    try {
      const url = `http://localhost:5095/Calculator/${operation}?a=${num1}&b=${num2}`;
      const response = await fetch(url);

      if (!response.ok) {
        const errMsg = await response.text();
        setResult("Error: " + errMsg);
        return;
      }

      const data = await response.text();
      setResult(data);

      await fetch("http://localhost:5095/CalculatorHistory", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          expression: `${num1} ${operation} ${num2}`,
          result: data
        })
      });
    } catch (error) {
      setResult("Error: Could not connect to backend");
      console.error(error);
    }
  };

  const handleKeyPress = (e) => {
    if (e.key === "Enter") handleCalculate();
  };

  return (
    
    <div className="container">
      <div className="calculator-main">
        <div className="calculator">
          <h1>CalcMate</h1>
          <input
            type="number"
            placeholder="First Number"
            value={num1}
            onChange={(e) => setNum1(e.target.value)}
            onKeyDown={handleKeyPress}
          />
          <input
            type="number"
            placeholder="Second Number"
            value={num2}
            onChange={(e) => setNum2(e.target.value)}
            onKeyDown={handleKeyPress}
          />
          <select value={operation} onChange={(e) => setOperation(e.target.value)}>
            <option value="add">Add</option>
            <option value="subtract">Subtract</option>
            <option value="multiply">Multiply</option>
            <option value="divide">Divide</option>
          </select>
          <button onClick={handleCalculate}>Calculate</button>
          
          
          <Link to="/history" style={{ marginTop: "10px", display: "inline-block" }}>
            Go To History
          </Link>

          {result !== null && <h2>Result: {result}</h2>}
        </div>
        <div className="image">
          <img src={calcImage} alt="Calculator" />
        </div>
      </div>
    </div>
  );
}

function App() {
  return (
    <Router>
      <div className="header">CalcMate</div>
      <Routes>
        <Route path="/" element={<Calculator />} />
        <Route path="/history" element={<HistoryPage />} />
      </Routes>
      <div className="footer"></div>
    </Router>
  );
}

export default App;*/

import React, { useState } from "react";
import { BrowserRouter as Router, Routes, Route, Link } from "react-router-dom";
import "./App.css";
import calcImage from "./assets/logo.png";
import HistoryPage from "./HistoryPage";

function Calculator() {
  const [num1, setNum1] = useState("");
  const [num2, setNum2] = useState("");
  const [operation, setOperation] = useState("add");
  const [result, setResult] = useState(null);

  const API_KEY = "my-secret-key"; // 🔑 Same as backend
  const BASE_URL = "http://localhost:5095";

  const handleCalculate = async () => {
    if (num1 === "" || num2 === "") {
      alert("Please enter both numbers");
      return;
    }

    try {
      const url = `${BASE_URL}/Calculator/${operation}?a=${num1}&b=${num2}`;
      const response = await fetch(url, {
        headers: { "X-API-KEY": API_KEY } // ✅ Added
      });

      if (!response.ok) {
        const errMsg = await response.text();
        setResult("Error: " + errMsg);
        return;
      }

      const data = await response.text();
      setResult(data);

      // Save calculation to history
      await fetch(`${BASE_URL}/CalculatorHistory`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          "X-API-KEY": API_KEY // ✅ Added
        },
        body: JSON.stringify({
          expression: `${num1} ${operation} ${num2}`,
          result: data
        })
      });
    } catch (error) {
      setResult("Error: Could not connect to backend");
      console.error(error);
    }
  };

  const handleKeyPress = (e) => {
    if (e.key === "Enter") handleCalculate();
  };

  return (
    <div className="container">
      <div className="calculator-main">
        <div className="calculator">
          <h1>CalcMate</h1>
          <input
            type="number"
            placeholder="First Number"
            value={num1}
            onChange={(e) => setNum1(e.target.value)}
            onKeyDown={handleKeyPress}
          />
          <input
            type="number"
            placeholder="Second Number"
            value={num2}
            onChange={(e) => setNum2(e.target.value)}
            onKeyDown={handleKeyPress}
          />
          <select value={operation} onChange={(e) => setOperation(e.target.value)}>
            <option value="add">Add</option>
            <option value="subtract">Subtract</option>
            <option value="multiply">Multiply</option>
            <option value="divide">Divide</option>
          </select>
          <button onClick={handleCalculate}>Calculate</button>

          {/* React Router navigation */}
          <Link to="/history" style={{ marginTop: "10px", display: "inline-block" }}>
            Go To History
          </Link>

          {result !== null && <h2>Result: {result}</h2>}
        </div>
        <div className="image">
          <img src={calcImage} alt="Calculator" />
        </div>
      </div>
    </div>
  );
}

function App() {
  return (
    <Router>
      <div className="header">CalcMate</div>
      <Routes>
        <Route path="/" element={<Calculator />} />
        <Route path="/history" element={<HistoryPage />} />
      </Routes>
      <div className="footer"></div>
    </Router>
  );
}

export default App;

