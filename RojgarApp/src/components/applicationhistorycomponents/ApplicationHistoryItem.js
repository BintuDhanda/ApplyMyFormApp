import React from 'react';
import {StyleSheet, Text, View} from 'react-native';
import {useNavigation} from '@react-navigation/native';
import {TouchableRipple} from 'react-native-paper';

const ApplicationHistoryItem = ({item, index}) => {
  const navigation = useNavigation();
  return (
    <TouchableRipple
      onPress={() =>
        navigation.navigate('ApplicationDetail', {id: item.id})
      }>
      <View key={index} style={styles.container}>
        <Text>{item.job.jobName}</Text>
        <Text>{item.job.jobStartDate}</Text>
        <Text>{item.job.jobEndDate}</Text>
      </View>
    </TouchableRipple>
  );
};

export default ApplicationHistoryItem;

const styles = StyleSheet.create({
  container: {
    padding: 5,
    flex: 1,
    backgroundColor: '#fff'
  },
});
