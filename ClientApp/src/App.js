import React, { Component } from 'react';
import { Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import { ImageList } from './components/ImageList';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route path='/' 
        render={ (params) => (
          <ImageList {...params}/>
        )} />
      </Layout>
    );
  }
}
