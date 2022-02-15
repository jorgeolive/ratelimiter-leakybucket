import http from 'k6/http';
import { sleep } from 'k6';
export const options = {
  vus: 20,
  duration: '20s',
};
export default function () {

  const params = {
    headers: {
      'Content-Type': 'application/json',
    },
  };

  http.post('http://localhost:5113/return-my-number', '1', params);
  sleep(0.5);
}