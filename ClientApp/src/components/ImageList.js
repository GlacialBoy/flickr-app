import queryString from 'query-string'
import React, { Component } from 'react';

import GridList from '@material-ui/core/GridList';
import GridListTile from '@material-ui/core/GridListTile';
import GridListTileBar from '@material-ui/core/GridListTileBar';
import ListSubheader from '@material-ui/core/ListSubheader';
import IconButton from '@material-ui/core/IconButton';
import InfoIcon from '@material-ui/icons/Info';

import { SearchBar } from './SearchBar';
import '../style/ImageList.css';

export class ImageList extends Component {

  constructor(props) {
    super(props);
    this.state = { data: [], loading: true };
    this.loadData = this.loadData.bind(this);
  }

  componentDidMount() {
    this.loadData();
  }

  loadData(query) {

    let url = "api/FlickrData/Images";
    if (query) {
      url = url + "?" + (new URLSearchParams({ query })).toString();
    }
    console.log(url);

    fetch(url)
      .then(response => response.json())
      .then(data => {
        this.setState({ data, loading: false });
      });
  }

  renderImages(dataItems) {
    return (
      <div>
        <SearchBar history={this.props.history} onSearch={this.loadData} />
        <div className={"gridListContainer"}>
          <GridList cols={4}>
            <GridListTile key="Subheader" cols={4} className={"gridList"} style={{ height: 'auto' }}>
              <ListSubheader component="div">Flick images collected from https://www.flickr.com/services/feeds/</ListSubheader>
            </GridListTile>

            {dataItems.map((item, index) => (
              <GridListTile key={index} cellheight="auto" onClick={() => { window.location.href = item.link; }}>
                <img className={"tileImage"} src={item.media.m} alt={item.title} />
                <GridListTileBar
                  title={item.title}
                  subtitle={<span>{item.tags ? "tags:" + item.tags : ""}</span>}
                  actionIcon={
                    <IconButton aria-label={`info about ${item.title}`}>
                      <InfoIcon />
                    </IconButton>
                  }
                />
              </GridListTile>
            ))}
          </GridList>
        </div>
      </div>
    );
  }

  render() {
    return (
      <div>
        {this.state.loading ? <p><em>Loading...</em></p> : this.renderImages(this.state.data)}
      </div>
    );
  }
}
