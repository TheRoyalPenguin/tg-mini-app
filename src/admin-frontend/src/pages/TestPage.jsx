import { useEffect } from 'react';
import getAvailableCourses from '../services/getAvailableCourses';

const TestPage = () => {
  useEffect(() => {
    const fetchCourses = async () => {
        const courses = await getAvailableCourses();
        console.log('Доступные курсы:', courses);
    };

    fetchCourses();
  }, []);

  return (
      <p>Работает</p>
  );
};

export default TestPage;