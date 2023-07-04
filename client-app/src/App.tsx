import React, { useEffect, useState } from 'react';
import './App.css';
import { Header, List, ListItem } from 'semantic-ui-react';
import axios from 'axios';

function App() {
  const [members, setMembers] = useState([]);

  useEffect(() => {
    axios.get('http://localhost:5023/api/members')
      .then(response => {
        setMembers(response.data);
      });
  }, []);

  return (
    <div>
      <Header as="h1" icon="hand spock" content="Opas Investments" />
      <List>
        {members.map((member: any) => (
          <ListItem key={member.id}>
            {member.lastName}, {member.firstName}
          </ListItem>
        ))} 
      </List>
    </div>
  );
}

export default App;
