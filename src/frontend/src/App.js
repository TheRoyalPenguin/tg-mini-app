import React, {useEffect} from 'react';
import CoursePage from './components/CoursePage';
import AuthPage from './components/AuthPage';
import WelcomePage from './components/WelcomePage';
import {fetchTest} from "./services/test";

function App() {
    useEffect(() => {
        const fetchData = async () => {
            await fetchTest();
        }
        fetchData();
    },[]);

  return (
    <div className="App">
      <AuthPage></AuthPage>
    </div>
  );
}

export default App;
