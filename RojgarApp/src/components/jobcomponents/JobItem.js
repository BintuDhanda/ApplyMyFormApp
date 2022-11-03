import {StyleSheet, Text, View} from 'react-native';
import React from 'react';
import {TouchableRipple} from 'react-native-paper';
import {useNavigation} from '@react-navigation/native';
import FontAwesome from 'react-native-vector-icons/dist/FontAwesome';

const JobItem = ({item, index}) => {
  const navigation = useNavigation();
  return (
    <TouchableRipple
      onPress={() => navigation.navigate('JobDetail', {id: item.jobId})}>
      <View key={index} style={styles.container}>
        <View style={styles.jobcard}>
          <Text style={styles.category}>{item.category.name}</Text>
          <Text style={styles.jobtitle}>{item.jobName}</Text>
          <Text style={styles.jobdesc}>
            {item.qualificationLevel} {item.jobDepartment}
          </Text>
          <View style={styles.lastrow}>
            <View style={styles.statusrow}>
              <Text>
                <FontAwesome style={styles.icon} name="calendar" />{' '}
                {item.jobPublishDate}
              </Text>
            </View>
            <View style={styles.statusrow}>
              <Text style={styles.statustext}>APPLY</Text>
            </View>
          </View>
        </View>
      </View>
    </TouchableRipple>
  );
};

export default JobItem;

const styles = StyleSheet.create({
  container: {
  },
  jobcard: {
    paddingLeft: 20,
    padding: 15,
    backgroundColor: 'white',
    borderRadius: 5,
  },
  category: {
    fontSize: 13,
    fontWeight: '700',
    color: '#4CA6A8',
  },
  jobtitle: {
    paddingTop: 2,
    paddingBottom: 2,
    fontSize: 20,
    fontWeight: '700',
    color: 'black',
  },
  jobdesc: {
    paddingTop: 2,
    paddingBottom: 2,
    fontSize: 14,
    fontWeight: '500',
  },
  lastrow: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    paddingTop: 12,
  },
  statusrow: {
    backgroundColor: '#dcefef',
    padding: 7,
    paddingLeft: 12,
    paddingRight: 12,
    borderRadius: 25,
    alignItems: 'center',
  },
  statustext: {
    fontSize: 13,
    fontWeight: '600',
  },
  icon: {
    fontSize: 15,
    color: '#4CA6A8',
  },
});
