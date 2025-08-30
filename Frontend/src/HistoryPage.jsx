import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import "./HistoryPage.css";

export default function HistoryPage() {
  const [history, setHistory] = useState([]);
  const [editingId, setEditingId] = useState(null);
  const [editCalc, setEditCalc] = useState({ num1: "", num2: "", operation: "add" });

  useEffect(() => {
    fetch("http://localhost:5095/CalculatorHistory")
      .then(res => res.json())
      .then(data => setHistory(data));
  }, []);

  const deleteCalc = async (id) => {
    await fetch(`http://localhost:5095/CalculatorHistory/${id}`, { method: "DELETE" });
    setHistory(history.filter(h => h.id !== id));
  };

  const startEdit = (calc) => {
    setEditingId(calc.id);
    const match = calc.expression.match(/(\d+)\s*([\+\-\*\/])\s*(\d+)/);
    if (match) {
      const [_, num1, op, num2] = match;
      setEditCalc({
        num1,
        num2,
        operation: op === "+" ? "add" : op === "-" ? "subtract" : op === "*" ? "multiply" : "divide"
      });
    }
  };

  const saveEdit = async (id) => {
    const a = parseFloat(editCalc.num1);
    const b = parseFloat(editCalc.num2);
    let result;
    switch (editCalc.operation) {
      case "add": result = a + b; break;
      case "subtract": result = a - b; break;
      case "multiply": result = a * b; break;
      case "divide": result = b !== 0 ? a / b : "Error"; break;
      default: result = "Error"; break;
    }

    const newExpression = `${editCalc.num1} ${editCalc.operation === "add" ? "+" :
      editCalc.operation === "subtract" ? "-" :
      editCalc.operation === "multiply" ? "*" : "/"} ${editCalc.num2}`;

    await fetch(`http://localhost:5095/CalculatorHistory/${id}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ expression: newExpression, result })
    });

    setHistory(history.map(h => h.id === id ? { ...h, expression: newExpression, result } : h));
    setEditingId(null);
  };

  return (
    <div className="history-container">
      <h1>Calculation History</h1>
      <Link to="/" className="go-back">Go To Calculator</Link>
      <ul className="history-list">
        {history.map(calc => (
          <li key={calc.id} className="history-item" data-testid={`history-${calc.id}`}>
            {editingId === calc.id ? (
              <>
                <input
                  type="number"
                  value={editCalc.num1}
                  onChange={(e) => setEditCalc({ ...editCalc, num1: e.target.value })}
                />
                <select
                  value={editCalc.operation}
                  onChange={(e) => setEditCalc({ ...editCalc, operation: e.target.value })}
                >
                  <option value="add">+</option>
                  <option value="subtract">-</option>
                  <option value="multiply">*</option>
                  <option value="divide">/</option>
                </select>
                <input
                  type="number"
                  value={editCalc.num2}
                  onChange={(e) => setEditCalc({ ...editCalc, num2: e.target.value })}
                />
                <div className="history-actions">
                  <button onClick={() => saveEdit(calc.id)}>Save</button>
                  <button onClick={() => setEditingId(null)}>Cancel</button>
                </div>
              </>
            ) : (
              <>
                <span>{calc.expression} = {calc.result}</span>
                <div className="history-actions">
                  <button onClick={() => startEdit(calc)}>Edit</button>
                  <button onClick={() => deleteCalc(calc.id)}>Delete</button>
                </div>
              </>
            )}
          </li>
        ))}
      </ul>
    </div>
  );
}
