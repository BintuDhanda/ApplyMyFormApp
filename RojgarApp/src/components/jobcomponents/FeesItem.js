import {FlatList, StyleSheet, Text, View} from 'react-native';
import React from 'react';
import {RadioButton, TouchableRipple} from 'react-native-paper';
import {useNavigation} from '@react-navigation/native';
import Colors from '../../settings/Colors';

const FeesItem = ({
  item,
  index,
  handleSelectPost,
  selectedPost,
  selectedPostId,
}) => {
  const navigation = useNavigation();
  return (
    // <TouchableRipple
    //   onPress={() => navigation.navigate('JobDetail', {id: item.jobId})}>
    <View key={index} style={styles.container}>
      <View style={styles.postcontainer}>
        <RadioButton
          color={Colors.primary}
          disabled={!selectedPostId.includes(item.jobPostId)}
          status={
            selectedPost.filter(
              g => g.postId === item.jobPostId && g.formFeeId === item.id,
            ).length > 0
              ? 'checked'
              : 'unchecked'
          }
          onPress={() => {
            handleSelectPost(item.jobPostId, item.id);
          }}
        />
        <Text style={styles.category}>{item.category}</Text>
      </View>
      <View style={styles.feescontainer}>
        <View style={styles.table}>
          <Text>Form fess</Text>
          <Text>Felling fees</Text>
          <Text>Admit fess</Text>
        </View>
        <View style={styles.tablefees}>
          <Text>{item.formFee}</Text>
          <Text>{item.formFillingFee}</Text>
          <Text>{item.admitCardFee}</Text>
        </View>
      </View>
    </View>
    // </TouchableRipple>
  );
};

export default FeesItem;

const styles = StyleSheet.create({
  container: {
    flex: 1,
  },
  postcontainer: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  category: {
    fontSize: 17,
    fontWeight: '500',
    color: '#4CA6A8',
  },
  table: {
    flexDirection: 'row',
    justifyContent: 'space-evenly',
  },
  tablefees: {
    flexDirection: 'row',
    justifyContent: 'space-around',
    paddingTop: 10,
  },
});
