import React, { Component } from 'react';
import Container from '@material-ui/core/Container';
import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';
import "../style/SearchBar.css";

export class SearchBar extends Component {

  constructor(props) {
    super(props);
    this.state = { currentCount: 0 };
    this.onSubmitHandler = this.onSubmitHandler.bind(this);
  }

  onSubmitHandler(event) {
    event.preventDefault();
    const query = event.target.elements.q.value;
    console.log(this.props);
    this.props.onSearch(query);
  }

  render() {
    return (
      <form onSubmit={this.onSubmitHandler} noValidate>
        <TextField
          id="standard-search"
          label="Search tags"
          type="search"
          margin="normal"
          name="q"
          className="searchText"
        />
        <Button className="searchButton" type="submit" >Search</Button>
      </form>
    );
  }
}
//<Container maxWidth="sm">