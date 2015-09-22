//
//  PrivacyViewController.m
//  IdeaBag-3
//
//  Created by Joe Petraglia on 9/21/15.
//  Copyright (c) 2015 IdeaBag. All rights reserved.
//

#import "PrivacyViewController.h"

@interface PrivacyViewController ()

@end

@implementation PrivacyViewController

- (void)viewDidLoad {
    [super viewDidLoad];
    // Do any additional setup after loading the view.
    
    
    UINavigationBar *bar = [self.navigationController navigationBar];
    [bar setTintColor:[UIColor whiteColor]];
    [bar setBarTintColor:[UIColor grayColor]];
    
    
    //[[UINavigationBar appearance]setTintColor:[UIColor blueColor]];
    //[[UINavigationBar appearance]setBarTintColor:[UIColor redColor]];
}

- (void)awakeFromNib {
    
   
    
}


- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

/*
#pragma mark - Navigation

// In a storyboard-based application, you will often want to do a little preparation before navigation
- (void)prepareForSegue:(UIStoryboardSegue *)segue sender:(id)sender {
    // Get the new view controller using [segue destinationViewController].
    // Pass the selected object to the new view controller.
}
*/

@end
