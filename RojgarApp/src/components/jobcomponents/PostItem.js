import {FlatList, StyleSheet, Text, View} from 'react-native';
import React, {useState} from 'react';
import {Checkbox} from 'react-native-paper';
import FeesItem from './FeesItem';
import Colors from '../../settings/Colors';
const PostItem = ({item, index, handleSelectPost, selectedPost}) => {
  const [selectedPostId, setSelectedPostId] = useState([]);
  const handleSelectedPostId = id => {
    let array = [...selectedPostId];
    let index = array.indexOf(id);
    if (index > -1) {
      array.splice(index, 1);
      let selectedPostArray = [...selectedPost];
      let filter = selectedPostArray.filter(g => g.postId === id);
      if (filter.length > 0) {
        handleSelectPost(filter[0].postId, filter[0].formFeeId);
      }
    } else {
      array.push(id);
    }
    setSelectedPostId(array);
  };
  return (
    <View key={index} style={styles.container}>
      <View style={{justifyContent: 'center', alignItems: 'center'}}>
        {item.isApplied && <Text style={{color: 'red'}}>Already applied</Text>}
      </View>
      <View style={styles.postnamecontainer}>
        <Text style={styles.postname}>Post Name :</Text>
        <Text style={styles.postsname}> {item.jobPostName}</Text>
      </View>
      <View style={styles.postcontainer}>
        <Checkbox
          disabled={item.isApplied}
          color={Colors.primary}
          onPress={() => handleSelectedPostId(item.jobPostId)}
          status={
            selectedPostId.includes(item.jobPostId) ? 'checked' : 'unchecked'
          }
        />
        <Text style={styles.posttitle}>Total Post :</Text>
        <Text style={styles.noofposts}>{item.numberOfPost}</Text>
        <Text style={styles.postsgender}>{item.jobPostGender}</Text>
      </View>

      <FlatList
        scrollEnabled={false}
        data={item.fees}
        renderItem={({item, index}) => (
          <FeesItem
            item={item}
            index={index}
            handleSelectPost={handleSelectPost}
            selectedPost={selectedPost}
            selectedPostId={selectedPostId}
          />
        )}
      />
    </View>
  );
};

export default PostItem;

const styles = StyleSheet.create({
  container: {
    padding: 5,
    paddingTop: 10,
    paddingBottom: 10,
    flex: 1,
    backgroundColor: '#fff',
    borderRadius: 5,
    marginTop: 10,
  },
  postcontainer: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  postnamecontainer: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
  },
  posttitle: {
    fontSize: 17,
    fontWeight: '500',
    color: '#4CA6A8',
  },
  postname: {
    fontSize: 18,
    fontWeight: '500',
    color: '#4CA6A8',
  },
  postsname: {
    fontSize: 17,
    fontWeight: '500',
    color: '#000000',
  },
  noofposts: {
    paddingLeft: 8,
    fontSize: 22,
    fontWeight: '500',
    color: 'black',
  },
  postsgender: {
    paddingLeft: 5,
    fontSize: 13,
    fontWeight: '500',
    color: 'black',
  },
});
